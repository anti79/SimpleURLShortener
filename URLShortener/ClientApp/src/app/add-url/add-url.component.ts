import { Component, EventEmitter, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrManager } from 'ng6-toastr-notifications';
import { ShortenResponse } from '../../models/shortenresponse';

import { URLRecord } from '../../models/urlrecord';
import { ShortenerService } from '../shortener.service';
import { URLNormalizerService } from '../urlnormalizer.service';

@Component({
  selector: 'app-add-url',
  templateUrl: './add-url.component.html',
  styleUrls: ['./add-url.component.css']
})
export class AddURLComponent {
  shortened = false;
  urlToShorten = "";
  urlResult = "";
  loading = false;
  alreadyExists = false;

  @Output()
  public onAdded: EventEmitter<URLRecord> = new EventEmitter();
  
  constructor(private toastr: ToastrManager, private route: ActivatedRoute, private router: Router,
    private shortenerService: ShortenerService, private urlNormalizer:URLNormalizerService) {
    
  }

  toastrConfig = { animate: "slideFromBottom", position: "bottom-right" }

  reset() {
    this.shortened = false;
    this.urlToShorten = "";
    this.urlResult = "";
    this.loading = false;
    this.alreadyExists = false;
  }

  copy() {
    
    navigator.clipboard.writeText(this.urlResult).then(
      () => {
        this.toastr.dismissAllToastr();
        this.toastr.successToastr('Link copied to clipboard', '', this.toastrConfig);
      },
      (err) => {
        this.toastr.errorToastr("Failed to copy the link", err, this.toastrConfig);
    })
  }

  displayInvalidURLError() {
    this.toastr.errorToastr("Invalid URL", '', this.toastrConfig);
  }

  shorten() {

    let url = "";
    try {
      url = this.urlNormalizer.normalize(this.urlToShorten);
    }
    catch (error) {
      this.displayInvalidURLError();
      return;
    }

    this.loading = true;
    this.shortenerService.shorten(url).subscribe({
      next: (response) => {
        console.log(response);
        const res = response as ShortenResponse;
        console.log(res);
        if (res.error) {
          
          return;
        }
        else if (res.status == "success") {
          this.shortened = true;
          this.onAdded.emit(res.result as URLRecord);
          if (res.alreadyExists) {
            this.alreadyExists = res.alreadyExists
          }
          else {
            this.alreadyExists = false;
          }
          const baseUrl = window.location.href;
          this.urlResult = baseUrl + res.result?.shortenedURL;
        }
      },
      error: (err) => {
        this.displayInvalidURLError()
        console.log(err);
        this.loading = false;
      },
      complete: () => {
        this.loading = false;

        
      }
    });
  }
}
