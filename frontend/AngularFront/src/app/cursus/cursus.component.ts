import { Component, OnInit } from '@angular/core';
import { CursusService } from '../cursus.service';
import { Cursus } from '../cursus';



@Component({
  selector: 'app-cursus',
  templateUrl: './cursus.component.html',
  styleUrls: ['./cursus.component.css']
})
export class CursusComponent implements OnInit {

  constructor(private cursusService: CursusService) { }

  ngOnInit(): void {
    this.getCursus();
  }

  cursussen: Cursus[] = [];

  getCursus(): void {
    this.cursusService.getCursus().subscribe(cursussen => this.cursussen = cursussen);
  }

}
