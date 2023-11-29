import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrManager } from 'ng6-toastr-notifications';
import { ShortenRequestResponse } from '../../models/requestresponse';
import { ShortenerService } from '../shortener.service';
import { URLNormalizerService } from '../urlnormalizer.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  shortened = false;
  urlToShorten = "";
  urlResult = "";
  loading = false;
  alreadyExists = false;

  constructor(private toastr: ToastrManager, private route: ActivatedRoute, private router: Router,
    private shortenerService: ShortenerService, private urlNormalizer:URLNormalizerService) {
    this.route.queryParams.subscribe(params => {
      if (params['redir']) {
        this.router.navigateByUrl(params['redir'])
      }
    });
  }

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
        this.toastr.successToastr('URL copied to clipboard', '', { animate:"slideFromBottom", position:"bottom-right"});
      },
      () => {

    })
  }

  displayInvalidURLError() {
    this.toastr.errorToastr("Invalid URL", '', { animate: "slideFromBottom", position: "bottom-right" });
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
        const res = response as ShortenRequestResponse;
        console.log(res);
        if (res.error) {
          
          return;
        }
        else if (res.status == "success") {
          this.shortened = true;
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
