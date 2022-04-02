<template>
    <v-overlay :value="loading" z-index="2000">
        <v-progress-circular indeterminate size="128" width="7" color="primary"></v-progress-circular>
    </v-overlay>
</template>

<script>
export default {
    name: "LoadingSpainner",

    data: () => ({
        loading: false,
    }),

    created() {
        this.$eventBus.$on("start-loading", this.StartLoading);
        this.$eventBus.$on("stop-loading", this.StopLoading);
    },

    methods: {
        StartLoading() {
            this.loading = true;
        },
        StopLoading() {
            this.loading = false;
        }
    }
}
</script>

<style>

</style>