const path = require("path");

module.exports = {
	entry: {
		app: "./src/App.ts"
	},
	output: {
		filename: "[name].js",
		path: __dirname + "/dist"
	},
	module: {
		rules: [
			{
				test: /\.ts?$/,
				loader: 'ts-loader',
				exclude: /node_modules/,
			},
			{
				test: /\.(png|jpg|gif|svg)$/,
				loader: 'file-loader',
				options: {
					name: '[name].[ext]?[hash]'
				}
			}
		]
	},
	resolve: {
		modules: [
			path.resolve(__dirname, "src")
		],
		extensions: ['.ts']
	},
	externals: {
		knockout: "ko",
		jQuery: "jquery",
		jss: "jss"
	}
}
