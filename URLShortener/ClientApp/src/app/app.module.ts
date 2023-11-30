import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { AddURLComponent } from './add-url/add-url.component';
import { ToastrModule } from 'ng6-toastr-notifications';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NotFoundComponent } from './not-found/not-found.component';
import { AboutComponent } from './about/about.component';
import { URLTableComponent } from './urltable/urltable.component';
import { LoginComponent } from './login/login.component';
import { CookieModule } from 'ngx-cookie';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    AddURLComponent,
    URLTableComponent,
    NotFoundComponent,
    AboutComponent,
    URLTableComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    FormsModule,
    CookieModule.withOptions(),
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: URLTableComponent, pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'not_found', component: NotFoundComponent },
      { path: 'about', component: AboutComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
