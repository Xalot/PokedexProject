import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';  // <--- Aquí importamos FormsModule
import { AppRoutingModule } from './app-routing.module';  
import { AuthService } from './services/auth.service';
import { AuthGuard } from './guards/auth.guard';
import { LoginComponent } from './components/login/login.component';
import { UsersComponent } from './components/users/users.component';  

@NgModule({
  declarations: [
    LoginComponent,
    UsersComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,  // <--- Asegúrate de que esté aquí también
    AppRoutingModule
  ],
  providers: [AuthService, AuthGuard],
  bootstrap: []
})
export class AppModule {}
