import * as ko from "knockout";

export const ComponentName: string = "NotFound";
export function Component() {
	return `component: {name: '${ComponentName}'}`;
}

ko.components.register(ComponentName, {
	template: `<div>Page Not Found</div>`,
});
