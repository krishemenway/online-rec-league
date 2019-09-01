import * as ko from "knockout";
import * as UrlRouterComponent from "UrlRouter/UrlRouterComponent";

export const RoutedComponent: UrlRouterComponent.RoutedComponent = {
	ComponentName: "LadderHome",
	CreateComponentParams: (matches) => ({ Path: matches[0] }),
	MatchRegex: /^\/ladder\/([^\/]+)$/i,
};

interface Params {
	Path: string;
}

class LadderHome {
	constructor(params: Params) {
		this.Path = params.Path;
	}

	public Path: string;
}

ko.components.register(RoutedComponent.ComponentName, {
	viewModel: LadderHome,
	template: `<div>Ladder Home <span data-bind="text: Path" /></div>`,
});
