import * as jQuery from "jquery";
import * as ko from "knockout";
import * as ComponentCleaner from "KnockoutHelpers/ComponentCleaner";
import * as AppStyles from "AppStyles";
import * as UrlRouterComponent from "UrlRouter/UrlRouterComponent";
import * as LeagueHomeComponent from "Leagues/LeagueHomeComponent";
import * as HomeComponent from "Home/HomeComponent";

ComponentCleaner.AddComponentCleaner();

let styles = AppStyles.createStyles("app", { });
let currentPathObservable = ko.observable<string>(window.location.pathname);

class AppViewModel {
	public RoutedComponents: UrlRouterComponent.RoutedComponent[] = [
		LeagueHomeComponent.RoutedComponent,
		HomeComponent.RoutedComponent,
	];

	public readonly CurrentPath: ko.Subscribable<string> = currentPathObservable;
}

ko.components.register("App", {
	viewModel: AppViewModel,
	template: `<div data-bind="${UrlRouterComponent.UrlRouterComponent("$component.RoutedComponents", "$component.CurrentPath")}" />`,
});

(window as any).initializeApp = (element: HTMLElement) => {
	element.setAttribute("data-bind", "component: {name: 'App'}");
	ko.applyBindings({}, element);
};

jQuery("body").on("click", "a", (event) => {
	let newPath = $(event.target).attr("href");

	history.pushState({}, "", newPath);
	currentPathObservable(newPath);

	event.preventDefault();
});
