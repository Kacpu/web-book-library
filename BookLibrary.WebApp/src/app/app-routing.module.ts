import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {BookListComponent} from "./components/book-list/book-list.component";
import {AuthFormComponent} from "./components/auth-form/auth-form.component";

const routes: Routes = [
  {path: '', component: BookListComponent},
  {path: 'login', component: AuthFormComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
