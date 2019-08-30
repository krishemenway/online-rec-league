import * as ko from "knockout";
import * as ComponentCleaner from "./KnockoutHelpers/ComponentCleaner";
import * as AppStyles from "./AppStyles";

ComponentCleaner.AddComponentCleaner();

class AppViewModel {

}

ko.components.register("RootApp", {
	viewModel: AppViewModel,
	template: `
		<div class="${AppStyles.padding.all}">
			Root App
		</div>`,
});

(window as any).initializeApp = (element: HTMLElement) => {
	element.setAttribute("data-bind", "component: {name: 'RootApp'}");
	ko.applyBindings({}, element);
};

