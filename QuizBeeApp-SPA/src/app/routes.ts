import {Routes} from '@angular/router'
import { HomeComponent } from './home/home.component';
import { EventComponent } from './event/event.component';
import { AuthGuard } from './_guard/auth.guard';
export const appRoutes: Routes = [
    {path: 'home',component: HomeComponent},
    {path: 'event',component:EventComponent,canActivate:[AuthGuard]},
    {path: '**',redirectTo: 'home',pathMatch:'full'}
]