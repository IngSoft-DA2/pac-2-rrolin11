import { Routes } from '@angular/router';
import { ReflectionComponent } from './pages/reflection/reflection/reflection.component';
import { ConsignaComponent } from './shared/components/consigna/consigna.component';
import { reflectionGuard } from './core/guards/reflection.guard';

export const routes: Routes = [
    {
        path: 'reflection',
        component: ReflectionComponent,
        title: 'Reflection',
        canActivate: [reflectionGuard]
    },
    {
        path: 'consigna',
        component: ConsignaComponent,
        title: 'Consigna'
    },
    {
        path: '',
        redirectTo: '/consigna',
        pathMatch: 'full'
    },
    {
        path: '**',
        redirectTo: '/consigna',
        pathMatch: 'full'
    }
];
