import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule }from'@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';

import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { TabsModule } from 'ngx-bootstrap/tabs';

import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale';
import { NgxCurrencyDirective } from "ngx-currency";

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';

import { ToastrModule } from 'ngx-toastr';

import { NgxSpinnerModule } from 'ngx-spinner';

import { LotService } from './services/lot.service';
import { EventService } from './services/event.service';
import { AccountService } from './services/account.service';

import { DateTimeFormatPipe } from './helpers/DateTimeFormat.pipe';

import { NavComponent } from './shared/nav/nav.component';
import { SharedTitleComponent } from './shared/shared-title/shared-title.component';

import { HomeComponent } from './components/home/home.component';
import { EventComponent } from './components/event/event.component';
import { SpeakerComponent } from './components/speaker/speaker.component';
import { SpeakerListComponent } from './components/speaker/speaker-list/speaker-list.component';
import { SpeakerDetailComponent } from './components/speaker/speaker-detail/speaker-detail.component';
import { ContactsComponent } from './components/contacts/contacts.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ProfileComponent } from './components/user/profile/profile.component';
import { ProfileDetailComponent } from './components/user/profile/profile-detail/profile-detail.component';
import { SocialNetworkComponent } from './components/socialNetwork/socialNetwork.component';
import { EventDetailComponent } from './components/event/event-detail/event-detail.component';
import { EventListComponent } from './components/event/event-list/event-list.component';
import { UserComponent } from './components/user/user.component';
import { LoginComponent } from './components/user/login/login.component';
import { RegistrationComponent } from './components/user/registration/registration.component';

import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { AuthGuard } from './guard/auth.guard';

defineLocale('pt-br', ptBrLocale);

@NgModule({
  declarations: [
    AppComponent,
    EventComponent,
    SpeakerComponent,
    SpeakerListComponent,
    SpeakerDetailComponent,
    ContactsComponent,
    DashboardComponent,
    ProfileComponent,
    ProfileDetailComponent,
    SocialNetworkComponent,
    SharedTitleComponent,
    NavComponent,
    HomeComponent,
    DateTimeFormatPipe,
    EventDetailComponent,
    EventListComponent,
    UserComponent,
    LoginComponent,
    RegistrationComponent,
  ],
  imports: [
    FormsModule,
    BrowserModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    CollapseModule.forRoot(),
    TooltipModule.forRoot(),
    BsDropdownModule.forRoot(),
    ModalModule.forRoot(),
    BsDatepickerModule.forRoot(),
    PaginationModule.forRoot(),
    TabsModule.forRoot(),
    ToastrModule.forRoot({
      timeOut: 5000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
      progressBar: true}),
    NgxSpinnerModule,
    NgxCurrencyDirective
  ],
  providers: [
    EventService,
    LotService,
    AccountService,
    AuthGuard,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true}
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
