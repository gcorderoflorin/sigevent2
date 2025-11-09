import { LoginComponent } from '@/account/login/login.component';
import { HomeComponent } from '@/home.component';
import { LandingComponent } from '@/pages/landing/landing.component';
import { Notfound } from '@/pages/notfound/notfound';
import { Routes } from '@angular/router';
import { AppLayout } from './app/layout/component/app.layout';
import { Dashboard } from './app/pages/dashboard/dashboard';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    {
        path: 'admin',
        component: AppLayout,
        children: [
            { path: '', component: Dashboard }
        ]
    },
    { path: 'login', component: LoginComponent },
    { path: 'landing', component: LandingComponent },
    { path: 'notfound', component: Notfound },
    { path: '**', redirectTo: '/notfound' }
];
