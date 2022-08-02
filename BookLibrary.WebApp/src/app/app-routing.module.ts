import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BookListComponent } from "./components/book-list/book-list.component";
import { AuthFormComponent } from "./components/auth-form/auth-form.component";
import { BookDetailsComponent } from "./components/book-details/book-details.component";

const routes: Routes = [
  {path: '', component: BookListComponent},
  {path: 'login', component: AuthFormComponent},
  {path: 'books/:bookId', component: BookDetailsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
