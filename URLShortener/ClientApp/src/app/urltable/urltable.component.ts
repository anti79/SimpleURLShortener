import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrManager } from 'ng6-toastr-notifications';
import { RecordsResponse } from '../../models/recordsresponse';
import { URLRecord } from '../../models/urlrecord';
import { AuthService } from '../auth.service';
import { RecordsService } from '../records.service';

@Component({
  selector: 'app-urltable',
  templateUrl: './urltable.component.html',
  styleUrls: ['./urltable.component.css']
})
export class URLTableComponent implements OnInit {

  records: URLRecord[] = [];
  baseURL = "";
  totalRecords = 0;
  constructor(private recordsService: RecordsService, private toastr: ToastrManager,
    protected authService:AuthService, private route:ActivatedRoute, private router:Router
  ) {
    this.baseURL = window.location.origin;
    this.route.queryParams.subscribe(params => {
      if (params['redir']) {
        this.router.navigateByUrl(params['redir'])
      }
    });
  }

  load() {
    this.recordsService.getMultiple(10).subscribe({

      next: (res) => {
        const result = res as RecordsResponse;
        this.records = result.records;
        this.totalRecords = result.total;
        console.log(this.records);
      },
      error: (err) => {
        this.toastr.errorToastr("Error fetching URLs", err, { animate: "slideFromBottom", position: "bottom-right" });
      }

    })
  }

  loadMore() {
    this.recordsService.getMultiple(10, this.records.length).subscribe({

      next: (res) => {
        const result = res as RecordsResponse;
        this.records = this.records.concat(result.records);
        console.log(this.records);
      },
      error: (err) => {
        this.toastr.errorToastr("Error fetching URLs", err, { animate: "slideFromBottom", position: "bottom-right" });
      }

    })
  }

  isThereMoreToLoad() {
    return this.records.length != this.totalRecords;
  }

  ngOnInit(): void {
    this.load();
  }

}
