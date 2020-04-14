import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { User } from "../../models/user.model";
import { UserService } from "../../services/user.service";
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-control',
  templateUrl: './user-control.component.html',
  styleUrls: ['./user-control.component.scss']
})
export class UserControlComponent implements OnInit {
  private user: User;
  public userForm: FormGroup;
  public saving: boolean;

  constructor(
    private userService: UserService,
    private fb: FormBuilder,
    private router:Router) {
    this.userForm = fb.group({
      'userFormFirstName': ['', Validators.required],
      'userFormLastName': ['', Validators.required],
      'userFormUsername': ['', Validators.required],
      'userFormPassword': ['', Validators.required]
    });
  }

  ngOnInit() {
  }

  public onSubmit() {
    this.saving = true;
    this.user = {
      firstName: this.userForm.value.userFormFirstName,
      lastName: this.userForm.value.userFormLastName,
      userName: this.userForm.value.userFormUsername,
      password: this.userForm.value.userFormPassword
    };

    this.userService.addUser(this.user).subscribe(() => {
      this.userForm.reset();
      this.saving = false;
      this.router.navigate(['/login']);
    });
  }

}
