import { URLRecord } from "./urlrecord";

type Status = "success" | "error";

export class RequestResponse {
  status: Status = "success";
  error: string | null = null;
}

export class ShortenRequestResponse extends RequestResponse
{
  result: URLRecord | null = null;
  alreadyExists: boolean | null = null;
}
