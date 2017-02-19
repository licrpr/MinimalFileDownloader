import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { ApiClientService } from './services/index';
import { NewDownloadsComponent } from './new-downloads/new-downloads.component';
import { ActiveDownloadsComponent } from './active-downloads/active-downloads.component';

const appRoutes: Routes = [
  { path: 'downloads/new', component: NewDownloadsComponent },
  { path: 'downloads/active', component: ActiveDownloadsComponent },  
  { path: '**', redirectTo: '/downloads/new' }
];

@NgModule({
  declarations: [
    AppComponent,
    NewDownloadsComponent,
    ActiveDownloadsComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [ApiClientService],
  bootstrap: [AppComponent]
})
export class AppModule { }
