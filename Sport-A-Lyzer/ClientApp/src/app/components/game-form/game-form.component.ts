import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { IMyOptions } from 'ng-uikit-pro-standard';
import { UUID } from 'angular2-uuid';
import { GameService } from '../../services/game.service'

@Component({
  selector: 'app-game-form',
  templateUrl: './game-form.component.html',
  styleUrls: ['./game-form.component.scss']
})
export class GameFormComponent implements OnInit {
  @Input() teams: any[];
  public gameForm: FormGroup;
  public saving: boolean;
  @Output() closeEvent = new EventEmitter();

  public gameDatePickerOptions: IMyOptions = {
    dayLabels: { mo: 'Ma', tu: 'Ti', we: 'Ke', th: 'To', fr: 'Pe', sa: 'La', su: 'Su', },
    dayLabelsFull: {
      mo: "Maanantai", tu: "Tiistai", we: "Keskiviikko", th: "Torstai", fr: "Perjantai",
      sa: "Lauantai", su: "Sunnuntai"
    },
    monthLabels: {
      1: 'Tammi', 2: 'Helmi', 3: 'Maalis', 4: 'Huhti', 5: 'Touko', 6: 'Kesä', 7: 'Heinä', 8: 'Elo', 9: 'Syys', 10:
        'Loka', 11: 'Marras', 12: 'Joulu'
    },
    monthLabelsFull: {
      1: "Tammikuu", 2: "Helmikuu", 3: "Maaliskuu", 4: "Huhtikuu", 5: "Toukokuu", 6: "Kesäkuu", 7: "Heinäkuu",
      8: "Elokuu", 9: "Syyskuu", 10: "Lokakuu", 11: "Marraskuu", 12: "Joulukuu"
    },
    dateFormat: "dd.mm.yyyy",
    firstDayOfWeek: "mo"
  };
  constructor(
    fb: FormBuilder,
    private gameService: GameService) {
    this.gameForm = fb.group({
      'gameFormHomeTeam': ['', Validators.required],
      'gameFormAwayTeam': ['', Validators.required],
      'gameFormDate': [''],
      'gameFormTime': [''],
      'gameFormPitch': [''],
      'gameFormDescription': ['']
    });
  }

  ngOnInit() {
  }

  onSubmit(): void {
    this.saving = true;

    var values = this.gameForm.value;
    var gameData = {
      HomeTeamId: values.gameFormHomeTeam,
      AwayTeamId: values.gameFormAwayTeam,
      GameDay: this.parseFinnishDate(values.gameFormDate)
    }

    this.gameService.upsertGame(UUID.UUID(), gameData).subscribe(() => {
      this.saving = false;
      this.closeEvent.emit();
    });
  }

  private parseFinnishDate(dateString) {
    var dateArray = dateString.split(".");
    return new Date(parseInt(dateArray[2]), parseInt(dateArray[1]) - 1, parseInt(dateArray[0]));
  }

}
