import { Routes } from '@angular/router';
import { IndexComponent } from './components/index/index.component';
import { AddComponent } from './components/add/add.component';
import { EditComponent } from './components/edit/edit.component';

export const routes: Routes = [

    {path:'',redirectTo:'index',pathMatch:'full'},
    {path:'index', component: IndexComponent},
    {path:'add', component: AddComponent},
    {path:'edit/:id', component: EditComponent},
];
