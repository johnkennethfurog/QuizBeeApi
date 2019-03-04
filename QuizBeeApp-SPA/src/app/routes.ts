import {Routes} from '@angular/router'
import { HomeComponent } from './home/home.component';
import { EventComponent } from './event/event.component';
import { AuthGuard } from './_guard/auth.guard';
import { EventDetailComponent } from './event-detail/event-detail.component';
import { QuestionBroadcastComponent } from './question-broadcast/question-broadcast.component';
export const appRoutes: Routes = [
    {path: 'home',component: HomeComponent},
    {path: 'event',component:EventComponent,canActivate:[AuthGuard]},
    {path: 'question-broadcast/:id',component:QuestionBroadcastComponent,canActivate:[AuthGuard]},
    {path: 'event-detail/:id',component:EventDetailComponent,canActivate:[AuthGuard]},
    {path: '**',redirectTo: 'home',pathMatch:'full'}
]