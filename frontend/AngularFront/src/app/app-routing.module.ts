import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CursusComponent } from './cursus/cursus.component';
import { FileUploadComponent } from './file-upload/file-upload.component';

const routes: Routes = [
  { path: 'Cursussen', component: CursusComponent },
  { path: 'Upload', component: FileUploadComponent }
];

@NgModule({
  
  imports: [
    RouterModule.forRoot(routes)],  
  exports: [RouterModule]
})
export class AppRoutingModule { }
