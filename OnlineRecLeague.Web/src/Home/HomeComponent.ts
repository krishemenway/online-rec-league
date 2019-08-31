import * as ko from "knockout";
import * as UrlRouterComponent from "UrlRouter/UrlRouterComponent";

export const RoutedComponent: UrlRouterComponent.RoutedComponent = {
	CreateComponentParams: (_) => {},
	ComponentName: "Home",
	MatchRegex: /^\/?$/i,
};

class Home {
}

ko.components.register(RoutedComponent.ComponentName, {
	viewModel: Home,
	template: `<div>Home</div>`,
});
