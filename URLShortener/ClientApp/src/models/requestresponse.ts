type Status = "success" | "error";

export class RequestResponse {
  status: Status = "success";
  error: string | null = null;
}

