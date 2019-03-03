import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule} from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms'
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { ValueComponent } from './value/value.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { AuthService } from './_services/auth.service';
import { RegisterComponent } from './register/register.component';
import { AlertifyService } from './_services/alertify.service';
import { BsDropdownModule, TabsModule, ModalModule } from 'ngx-bootstrap';
import { EventComponent } from './event/event.component';
import { appRoutes } from './routes';
import { AuthGuard } from './_guard/auth.guard';
import { EventService } from './_services/event.service';
import { EventCardComponent } from './event-card/event-card.component';
import { EventDetailComponent } from './event-detail/event-detail.component';
import { QuestionCreateComponent } from './question-create/question-create.component';
import { CategoryService } from './_services/category.service';
import { QuestionService } from './_services/question.service';
import { EmitterService } from './_services/emitter.service';
import { ParticipantService } from './_services/participant.service';
import { ParticipantCreateComponent } from './participant-create/participant-create.component';

@NgModule({
   declarations: [
      AppComponent,
      ValueComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent,
      EventComponent,
      EventCardComponent,
      EventDetailComponent,
      QuestionCreateComponent,
      ParticipantCreateComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      BsDropdownModule.forRoot(),
      RouterModule.forRoot(appRoutes),
      TabsModule.forRoot(),
      ModalModule.forRoot(),
      ReactiveFormsModule
   ],
   providers: [
      AuthService,
      AlertifyService,
      AuthGuard,
      EventService,
      CategoryService,
      QuestionService,
      EmitterService,
      ParticipantService
   ],
   bootstrap: [
      AppComponent
   ],
   entryComponents:[
      ParticipantCreateComponent
   ]
})
export class AppModule { }
