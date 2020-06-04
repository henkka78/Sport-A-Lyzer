import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { AnalyzerComponent } from './analyzer.component';


const routes: Routes = [
  { path: '', component: AnalyzerComponent }
];

@NgModule({
  declarations: [AnalyzerComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class AnalyzerModule { }
