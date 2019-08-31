import * as ko from "knockout";
import * as NotFound from "UrlRouter/NotFoundComponent";

var ComponentName : string = "UrlRouter";
export function UrlRouterComponent(routedComponentsPropertyName: string, currentPathPropertyName: string) {
	return `component: {name: '${ComponentName}', params: {RoutedComponents: ${routedComponentsPropertyName}, CurrentPath: ${currentPathPropertyName}}}`;
}

export interface Routes {
	CurrentPath: ko.Subscribable<string>;
	RoutedComponents: RoutedComponent[];
}

export interface RoutedComponent {
	MatchRegex: RegExp;
	ComponentName: string;
	CreateComponentParams: (regexMatches: string[]) => any;
}

export class UrlRouter {
	constructor(params: Routes) {
		this.CurrentPath = params.CurrentPath;
		this.RoutedComponents = params.RoutedComponents;

		this.CurrentRoutedComponent = ko.pureComputed(this.CurrentRoutedComponentFunc, this);
		this.CurrentRoutedComponentName = ko.pureComputed(this.CurrentRoutedComponentNameFunc, this);
		this.CurrentRoutedComponentParams = ko.pureComputed(this.CurrentRoutedComponentParamsFunc, this);
	}

	private CurrentRoutedComponentFunc() {
		for (let i = 0; i < this.RoutedComponents.length; i++) {
			let route = this.RoutedComponents[i];

			if (route.MatchRegex.test(this.CurrentPath())) {
				return route;
			}
		}

		return null;
	}

	private CurrentRoutedComponentNameFunc(){
		let route = this.CurrentRoutedComponent();
		return route !== null ? route.ComponentName : NotFound.ComponentName;
	}

	private CurrentRoutedComponentParamsFunc() {
		let route = this.CurrentRoutedComponent();

		if (route === null) {
			return {};
		}

		let matchResults = (this.CurrentPath().match(route.MatchRegex) as string[]).slice(1);
		return route.CreateComponentParams(matchResults);
	}

	private CurrentRoutedComponent: ko.Computed<RoutedComponent>;
	private CurrentRoutedComponentName: ko.Computed<string>;
	private CurrentRoutedComponentParams: ko.Computed<any>;

	private CurrentPath: ko.Subscribable<string>;
	private RoutedComponents: RoutedComponent[];
}

ko.components.register("UrlRouter", {
	viewModel: UrlRouter,
	template: `
		<!-- ko if: CurrentRoutedComponentName() -->
		<div data-bind="component: {name: CurrentRoutedComponentName(), params: CurrentRoutedComponentParams()}" />
		<!-- /ko -->
	`,
});
