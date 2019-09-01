import * as ko from "knockout";
import * as UrlRouterComponent from "UrlRouter/UrlRouterComponent";

export const RoutedComponent: UrlRouterComponent.RoutedComponent = {
	ComponentName: "LeagueHome",
	CreateComponentParams: (matches) => ({ Path: matches[0] }),
	MatchRegex: /^\/league\/([^\/]+)$/i,
};

interface Params {
	Path: string;
}

class LeagueHome {
	constructor(params: Params) {
		this.Path = params.Path;
	}

	public Path: string;
}

ko.components.register(RoutedComponent.ComponentName, {
	viewModel: LeagueHome,
	template: `<div>League Home <span data-bind="text: Path" /></div>`,
});
