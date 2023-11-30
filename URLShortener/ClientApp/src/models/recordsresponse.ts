import { RequestResponse } from "./requestresponse";
import { URLRecord } from "./urlrecord";

export class RecordsResponse extends RequestResponse {
  records: URLRecord[] = [];
  total: number = 0;
}
