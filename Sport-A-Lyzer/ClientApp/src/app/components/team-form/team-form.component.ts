import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Team } from '../../models/team.model';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { TeamService } from '../../services/team.service';
import { UUID } from 'angular2-uuid';

@Component({
  selector: 'app-team-form',
  templateUrl: './team-form.component.html',
  styleUrls: ['./team-form.component.scss']
})
export class TeamFormComponent implements OnInit {

  @Input() team: Team;
  @Input() isNew: boolean;
  @Output() closeEvent = new EventEmitter();
  public teamForm: FormGroup;
  public saving: boolean;

  constructor(
    private teamService: TeamService,
    private fb: FormBuilder
  ) {
    this.teamForm = fb.group({
      'teamFormName': ['', Validators.required]
    });
  }

  ngOnInit() {
    if (!this.isNew) {
      this.teamForm.setValue(
        {
          teamFormName: this.team.name
        });
    }
  }

  onSubmit() {
    this.saving = true;
    let teamId = this.isNew ? UUID.UUID() : this.team.teamId;
    let team = {
      Name: this.teamForm.value.teamFormName
    }
    this.teamService.putTeam(teamId, team).subscribe(() => {
      this.teamForm.reset();
      this.saving = false;
      this.closeEvent.emit();
    });
  }


}
