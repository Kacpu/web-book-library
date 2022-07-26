import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {AuthFormComponent} from "./auth-form/auth-form.component";
import {BookListComponent} from "./book-list/book-list.component";

const routes: Routes = [
  {path: '', component: BookListComponent},
  {path: 'login', component: AuthFormComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
