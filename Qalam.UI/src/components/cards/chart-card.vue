<template>
	<custom-card v-bind="$attrs" class="v-card--material-chart" v-on="$listeners" :height="height" >
		<chartist slot="header" :data="data" :event-handlers="eventHandlers" :options="options" :ratio="ratio" :responsive-options="responsiveOptions" :type="type"/>

		<slot />

		<slot slot="actions" name="actions"/>
	</custom-card>
</template>

<script>
import CustomCard from './custom-card.vue'

export default {
	name: "ChartCard",

	components: {
		CustomCard
	},

	inheritAttrs: false,

	props: {
		data: {
			type: Object,
			default: () => ({})
		},
		eventHandlers: {
			type: Array,
			default: () => ([])
		},
		options: {
			type: Object,
			default: () => ({})
		},
		ratio: {
			type: String,
			default: "1"
		},
		responsiveOptions: {
			type: Array,
			default: () => ([])
		},
		type: {
			type: String,
			required: true,
			validator: v => ['Bar', 'Line', 'Pie'].includes(v)
		},
		height: {
			type: [Number, String],
			default: undefined
		}
	}
}
</script>

<style lang="scss">
.v-card--material-chart {
	.v-card--material__header {
		.ct-label {
			color: inherit;
			opacity: .7;
			font-size: 0.975rem;
			font-weight: 100;
		}

		.ct-grid{
			stroke: rgba(255, 255, 255, 0.2);
		}
		.ct-series-a .ct-point,
		.ct-series-a .ct-line,
		.ct-series-a .ct-bar,
		.ct-series-a .ct-slice-donut {
			stroke: rgba(255,255,255,.8);
		}
		.ct-series-a .ct-slice-pie,
		.ct-series-a .ct-area {
			fill: rgba(255,255,255,.4);
		}
	}
}
</style>
