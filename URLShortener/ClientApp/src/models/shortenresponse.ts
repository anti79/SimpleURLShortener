import { RequestResponse } from "./requestresponse";
import { URLRecord } from "./urlrecord";

export class ShortenResponse extends RequestResponse {
  result: URLRecord | null = null;
  alreadyExists: boolean | null = null;
}
