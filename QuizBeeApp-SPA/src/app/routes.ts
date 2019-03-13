import {Routes} from '@angular/router'
import { HomeComponent } from './home/home.component';
import { EventComponent } from './event/event.component';
import { AuthGuard } from './_guard/auth.guard';
import { EventDetailComponent } from './event-detail/event-detail.component';
import { QuestionBroadcastComponent } from './question-broadcast/question-broadcast.component';
import { QuestionDisplayComponent } from './question-display/question-display.component';
import { JudgeWallComponent } from './judge-wall/judge-wall.component';
import { JudgeLoginComponent } from './judge-login/judge-login.component';
export const appRoutes: Routes = [
    {path: 'home',component: HomeComponent},
    {path: 'judge-wall',component:JudgeWallComponent},
    {path: 'event',component:EventComponent,canActivate:[AuthGuard]},
    {path: 'question-broadcast/:id',component:QuestionBroadcastComponent,canActivate:[AuthGuard]},
    {path: 'event-detail/:id',component:EventDetailComponent,canActivate:[AuthGuard]},
    {path: 'display',component:QuestionDisplayComponent},
    {path: 'judge-login',component:JudgeLoginComponent},
    {path: '**',redirectTo: 'home',pathMatch:'full'}
]