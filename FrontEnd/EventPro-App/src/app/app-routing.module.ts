import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DashboardComponent } from './components/dashboard/dashboard.component';
import { SpeakerComponent } from './components/speaker/speaker.component';

import { EventComponent } from './components/event/event.component';
import { EventListComponent } from './components/event/event-list/event-list.component';
import { EventDetailComponent } from './components/event/event-detail/event-detail.component';

import { UserComponent } from './components/user/user.component';
import { LoginComponent } from './components/user/login/login.component';
import { RegistrationComponent } from './components/user/registration/registration.component';
import { ProfileComponent } from './components/user/profile/profile.component';


import { ContactsComponent } from './components/contacts/contacts.component';
import { AuthGuard } from './guard/auth.guard';
import { HomeComponent } from './components/home/home.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'user', redirectTo: 'user/profile' },
      { path: 'user/profile', component: ProfileComponent },
      { path: 'events', redirectTo: 'events/list' },
      { path: 'events', component: EventComponent,
        children:[
          { path: 'detail/:id', component: EventDetailComponent },
          { path: 'detail', component: EventDetailComponent },
          { path: 'list', component: EventListComponent },
        ]
      },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'speakers', component: SpeakerComponent },
      { path: 'contacts', component: ContactsComponent },
    ]
  },
  {
    path: 'user', component: UserComponent,
    children:[
      { path: 'login', component: LoginComponent },
      { path: 'registration', component: RegistrationComponent },
    ]
  },
  { path: 'home', component: HomeComponent },
  { path: '**', redirectTo: 'home', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
