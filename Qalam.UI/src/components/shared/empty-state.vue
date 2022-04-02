<template>
    <v-container fluid class="justify-center align-center text-center" v-bind="$attrs">
        <v-card class="transparent elevation-0">
            <img src="@/assets/images/empty state.png" alt="Empty State" class="img-fluid" id="emptyState" />
            <v-card-subtitle class="title text-center">
                <h2>{{ message || $locales.t('noData') }}</h2>
            </v-card-subtitle>

            <v-card-actions class="ma-4 align-content-center justify-center text-center">
                <slot></slot>
            </v-card-actions>
        </v-card>
    </v-container>
</template>

<script>
export default {
    name: "EmptyeState",
    props: {
        message: {
            type: String,
        }
    }
}
</script>

<style scoped>
.img-fluid {
    max-width: 100%;
    height: auto;
}
</style>