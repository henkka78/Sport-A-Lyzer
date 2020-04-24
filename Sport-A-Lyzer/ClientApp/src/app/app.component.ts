import { Component } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
import { AuthenticationService } from "./services/authentication.service";
import { User } from "./models/user.model";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  currentUser: User;
  public userName:string;
  constructor(
    private location: Location,
    private router: Router,
    private authenticationService: AuthenticationService
  ) {
    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
    console.log(this.currentUser);
    if (this.currentUser !== null) {
      this.userName = this.currentUser.firstName + " " + this.currentUser.lastName;
    }
  }

  public logout() {
    this.authenticationService.logout();
    this.router.navigate(['/login']);
  }

  public getActiveRoute(url: string) {
    return this.location.path() === url ? "active-subitem" : "";
  }

  get isLoggedIn(): boolean {
    return this.currentUser !== null;
  }

  //get userName(): string {
  //  return this.currentUser.username;
  //}



  
}
