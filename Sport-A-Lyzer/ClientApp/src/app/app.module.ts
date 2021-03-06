import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AgmCoreModule } from '@agm/core';
import { AppComponent } from './app.component';
import { RouterModule, Routes } from '@angular/router';
import { MDBSpinningPreloader, MDBBootstrapModulesPro, ToastModule } from 'ng-uikit-pro-standard';
import { PlayerFormComponent } from './components/player-form/player-form.component';
import { TeamFormComponent } from './components/team-form/team-form.component';
import { GameFormComponent } from './components/game-form/game-form.component';
import { TeamComponent } from './components/team/team.component';
import { GameAdminComponent } from './components/game/game-admin.component';
import { LoginComponent } from './components/login/login.component';
import { JwtInterceptor } from './helpers/jwt.interceptor'
import { ErrorInterceptor } from './helpers/error.interceptor'
import { AuthGuard } from './helpers/auth.guard';
import { HomeComponent } from './components/home/home.component';
import { UserControlComponent } from './components/user-control/user-control.component';
import { GameFollowComponent } from './components/game-follow/game-follow.component';
import { LoaderComponent } from './components/loader/loader.component';
import { WhatsAppButtonComponent } from './components/whats-app-button/whats-app-button.component';
import { ClipboardModule } from 'ngx-clipboard';
import { DeviceDetectorModule } from 'ngx-device-detector';
import { TimerComponent } from './components/timer/timer.component';

const appRoutes: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'team-admin', component: TeamComponent, canActivate: [AuthGuard] },
  { path: 'game-admin', component: GameAdminComponent, canActivate: [AuthGuard] },
  { path: 'game-follow', component: GameFollowComponent, canActivate: [AuthGuard] },
  { path: 'user-control', component: UserControlComponent, canActivate: [AuthGuard] },
  { path: 'register', component: UserControlComponent },
  { path: 'login', component: LoginComponent },
  { path: 'admin', loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule) }
];

@NgModule({
  declarations: [
    AppComponent,
    PlayerFormComponent,
    TeamFormComponent,
    GameFormComponent,
    TeamComponent,
    GameAdminComponent,
    LoginComponent,
    HomeComponent,
    UserControlComponent,
    GameFollowComponent,
    LoaderComponent,
    WhatsAppButtonComponent,
    TimerComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ClipboardModule,
    DeviceDetectorModule.forRoot(),
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: false }
    ),
    ToastModule.forRoot(),
    MDBBootstrapModulesPro.forRoot(),
    AgmCoreModule.forRoot({
      // https://developers.google.com/maps/documentation/javascript/get-api-key?hl=en#key
      apiKey: 'Your_api_key'
    })
  ],
  providers: [MDBSpinningPreloader,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }],
  bootstrap: [AppComponent],
  schemas: [NO_ERRORS_SCHEMA]
})
export class AppModule { }
