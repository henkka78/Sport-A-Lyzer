import { Component, } from '@angular/core';
import { LoaderService } from "../../services/loader.service";
import { Subject } from 'rxjs';

@Component({
  selector: 'app-loader',
  templateUrl: './loader.component.html',
  styleUrls: ['./loader.component.scss']
})
export class LoaderComponent {

  constructor(private loaderService: LoaderService) { }

  public loading: Subject<boolean> = this.loaderService.isLoading;
}
