import { Component, OnInit, Input } from '@angular/core';
import { timer, Observable, Subscription } from 'rxjs';
import { DeviceDetectorService } from 'ngx-device-detector';

@Component({
  selector: 'app-timer',
  templateUrl: './timer.component.html',
  styleUrls: ['./timer.component.scss']
})
export class TimerComponent implements OnInit {
  @Input() secondsPlayed: number;
  @Input() gamePaused: boolean;
  @Input() gameEnded: boolean;
  @Input() startClockEvent: Observable<void>;

  public minutes: number = 0;
  public seconds: number = 0;
  public secondsString: string;
  public gameTimer = timer(0, 1000);
  public timerSubscription;
  private focusLost: boolean;
  private isMobile: boolean;
  private startClockSubscription: Subscription;

  constructor(
    private deviceService: DeviceDetectorService
  ) { }

  ngOnInit(): void {
    this.isMobile = this.deviceService.isMobile();
    this.startClockSubscription = this.startClockEvent.subscribe(() => this.startClock());
  }

  private startClock() {
    this.timerSubscription = this.gameTimer.subscribe(value => {
      if (!document.hasFocus() && !this.focusLost) {
        this.focusLost = true;
        let focusLostInfo = {
          moment: new Date(),
          secondsPlayed: this.secondsPlayed
        };
        window.localStorage.setItem("focusLostInfo", JSON.stringify(focusLostInfo));
      }
      if (document.hasFocus() && this.focusLost && this.isMobile) {
        let focusLostInfo = JSON.parse(window.localStorage.getItem("focusLostInfo"));
        let secondsSinceFocusLost = Math.floor(
          (new Date().getTime() - new Date(focusLostInfo.moment).getTime()) / 1000);

        this.focusLost = false;
        this.timerSubscription.unsubscribe();
        this.setClock(focusLostInfo.secondsPlayed + secondsSinceFocusLost);
      }

      this.secondsPlayed++;
      this.seconds++;
      if (this.seconds === 60) {
        this.minutes++;
        this.seconds = 0;
      }
      this.secondsString = this.seconds.toString().padStart(2, "0");
    });
  }

  private setClock(secondsPlayed: number): void {
    this.secondsPlayed = secondsPlayed;
    this.minutes = Math.floor(this.secondsPlayed / 60);
    this.seconds = this.secondsPlayed % 60;
    this.startClock();
  }

}
