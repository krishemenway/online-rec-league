import * as $ from "jquery";
import * as ko from "knockout";

export interface IJsonRequestProxy {
	Get<TResponse>(url: string, isLoading: ko.Observable<boolean>): ResponseHandler<TResponse>;
	Post<TRequest, TResponse>(url: string, data: TRequest, isLoading: ko.Observable<boolean>): ResponseHandler<TResponse>;
}

export class JsonRequestProxy implements IJsonRequestProxy {
	public Get<TResponse>(url: string, isLoading: ko.Observable<boolean>): ResponseHandler<TResponse> {
		let builder = new ResponseHandler<TResponse>();

		isLoading(true);
		$.get(url)
		 .done((response: TResponse) => builder.FireOnSuccess(response))
		 .fail((xhr, e, a) => {
			if (xhr.responseJSON.hasOwnProperty("Errors") && !!xhr.responseJSON.Errors) {
				builder.FireOnFailure(xhr.responseJSON.Errors[0]);
			} else {
				builder.FireOnFailure("Something went wrong with request, please try again later.");
			}
		 })
		 .always(() => isLoading(false))

		return builder;
	}

	public Post<TRequest, TResponse>(url: string, data: TRequest, isLoading: ko.Observable<boolean>): ResponseHandler<TResponse> {
		let builder = new ResponseHandler<TResponse>();

		isLoading(true);
		$.post(url, JSON.stringify(data))
		 .done((response: TResponse) => builder.FireOnSuccess(response))
		 .fail((xhr, e, a) => {
			if (xhr.responseJSON.hasOwnProperty("Errors") && !!xhr.responseJSON.Errors) {
				builder.FireOnFailure(xhr.responseJSON.Errors[0]);
			} else {
				builder.FireOnFailure("Something went wrong with request, please try again later.");
			}
		})
		 .always(() => isLoading(false))

		return builder;
	}
}

export class ResponseHandler<TResponse> {
	public OnSuccess(onSuccessFunc: (response: TResponse) => void): ResponseHandler<TResponse> {
		this.OnSuccessFunc = onSuccessFunc;
		return this;
	}

	public FireOnSuccess(response: TResponse) {
		this.OnSuccessFunc(response);
	}

	public OnFailure(onFailureFunc: (error: string) => void): ResponseHandler<TResponse> {
		this.OnFailureFunc = onFailureFunc;
		return this;
	}

	public FireOnFailure(error: string) {
		this.OnFailureFunc(error);
	}

	private OnSuccessFunc: (response: TResponse) => void;
	private OnFailureFunc: (error: string) => void;
}
