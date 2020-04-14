import { Component, OnInit, HostListener, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { TeamService } from '../../services/team.service';
import { UUID } from 'angular2-uuid';
import { PlayerModel } from '../../models/player.model'

@Component({
  selector: 'app-player-form',
  templateUrl: './player-form.component.html',
  styleUrls: ['./player-form.component.scss']
})
export class PlayerFormComponent implements OnInit {

  @Input() player: PlayerModel;
  @Input() teamId: string;
  @Input() isNew: boolean;
  @Output() closeEvent = new EventEmitter();
  public playerForm: FormGroup;
  disabledSubmitButton: boolean = true;
  public teams: any[];
  public saving = false;
  public header: string;

  //@HostListener('input') oninput() {

  //  if (this.playerForm.valid) {
  //    this.disabledSubmitButton = false;
  //  }
  //}

  constructor(
    fb: FormBuilder,
    private teamService: TeamService) {
    this.playerForm = fb.group({
      'playerFormFirstName': ['', Validators.required],
      'playerFormLastName': ['', Validators.required],
      'playerFormNumber': ['', Validators.required]
    });

  }

  ngOnInit() {
    if (!this.isNew) {
      this.header = "Muokkaa pelaajan tietoja";
      this.playerForm.setValue(
        {
          playerFormFirstName: this.player.firstName,
          playerFormLastName: this.player.lastName,
          playerFormNumber: this.player.playerNumber
        });
    } else {
      this.header = "Uusi pelaaja";
    }
  }

  get firstName() {
    return this.playerForm.get('materialPlayerFormFirstName');
  }
  get lastName() {
    return this.playerForm.get('materialPlayerFormLastName');
  }


  onSubmit() {
    this.saving = true;
    let playerId = this.isNew ? UUID.UUID() : this.player.playerId;
    let player = {
      LastName: this.playerForm.value.playerFormLastName,
      FirstName: this.playerForm.value.playerFormFirstName,
      Number: this.playerForm.value.playerFormNumber,
      TeamId: this.teamId
    }
    this.teamService.putPlayer(playerId, player).subscribe(() => {
      this.playerForm.reset();
      this.saving = false;
      this.closeEvent.emit();
    });
  }


}
