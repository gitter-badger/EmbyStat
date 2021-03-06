import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { TranslateModule } from '@ngx-translate/core';
import { DashboardComponent } from './dashboard.component';

@NgModule({
  imports: [
    CommonModule,
    TranslateModule,
    SharedModule
  ],
  declarations: [DashboardComponent]
})
export class DashboardModule { }
