import { BrowserModule } from '@angular/platform-browser';
import { NgModule, enableProdMode } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpModule } from '@angular/http';
import { AuthGuard } from './services/auth-guard.service';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ModalModule } from 'ngx-bootstrap/modal';
import { DropdownModule } from "ngx-dropdown";
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClient, HttpHandler, HTTP_INTERCEPTORS } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { AllblogsComponent } from './components/allblogs/allblogs.component';
import { BlogComponent } from './components/add-update-view/blog.component';
import { BlogPostService} from './services/blog-post.service'
import { NgxEditorModule } from 'ngx-editor';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { InterceptService } from 'src/config/intercept.service';
import { ToastrModule } from 'ngx-toastr';

enableProdMode();

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    AllblogsComponent,
    BlogComponent 
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    BsDropdownModule.forRoot(),
    TooltipModule.forRoot(),
    ModalModule.forRoot(),
    DropdownModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,
    ToastrModule.forRoot(),
    RouterModule.forRoot([
    ]),
    NgxEditorModule,
    AngularFontAwesomeModule,    
  ],
  exports: [BsDropdownModule, TooltipModule, ModalModule],
  providers: [
    InterceptService,
		{
			provide: HTTP_INTERCEPTORS,
			useClass: InterceptService,
			multi: true
		},
    HttpClient, AuthGuard, BlogPostService],
  bootstrap: [AppComponent]
})
export class AppModule {


}

