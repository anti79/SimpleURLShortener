"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (Object.prototype.hasOwnProperty.call(b, p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
exports.ShortenRequestResponse = exports.RequestResponse = void 0;
var RequestResponse = /** @class */ (function () {
    function RequestResponse() {
        this.status = "success";
        this.error = null;
    }
    return RequestResponse;
}());
exports.RequestResponse = RequestResponse;
var ShortenRequestResponse = /** @class */ (function (_super) {
    __extends(ShortenRequestResponse, _super);
    function ShortenRequestResponse() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.result = null;
        _this.alreadyExists = null;
        return _this;
    }
    return ShortenRequestResponse;
}(RequestResponse));
exports.ShortenRequestResponse = ShortenRequestResponse;
//# sourceMappingURL=requestresponse.js.map