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
exports.RecordsResponse = void 0;
var requestresponse_1 = require("./requestresponse");
var RecordsResponse = /** @class */ (function (_super) {
    __extends(RecordsResponse, _super);
    function RecordsResponse() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.records = [];
        _this.total = 0;
        return _this;
    }
    return RecordsResponse;
}(requestresponse_1.RequestResponse));
exports.RecordsResponse = RecordsResponse;
//# sourceMappingURL=recordsresponse.js.map