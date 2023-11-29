import { Component, OnInit } from '@angular/core';
import { ToastrManager } from 'ng6-toastr-notifications';
import { RecordsResponse } from '../../models/recordsresponse';
import { URLRecord } from '../../models/urlrecord';
import { RecordsService } from '../records.service';

@Component({
  selector: 'app-urltable',
  templateUrl: './urltable.component.html',
  styleUrls: ['./urltable.component.css']
})
export class URLTableComponent implements OnInit {

  records: URLRecord[] = [];
  baseURL = "";
  constructor(private recordsService: RecordsService, private toastr: ToastrManager) {
    this.baseURL = window.location.origin;
  }

  load() {
    this.recordsService.getMultiple(10).subscribe({

      next: (res) => {
        const result = res as RecordsResponse;
        this.records = result.records;
        console.log(this.records);
      },
      error: (err) => {
        this.toastr.errorToastr("Error fetching URLs", err, { animate: "slideFromBottom", position: "bottom-right" });
      }

    })
  }

  ngOnInit(): void {
    this.load();
  }

}
