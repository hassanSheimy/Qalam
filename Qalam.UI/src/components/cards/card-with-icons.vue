<template>
    <v-col v-bind="$attrs">
        <v-card class="justify-center ma-1" min-height="216">
            <v-list-item>
                <v-list-item-content>
                    <v-list-item-title class="headline">{{title}}</v-list-item-title>
                    <v-list-item-subtitle v-if="subtitle">{{subtitle}}</v-list-item-subtitle>
                </v-list-item-content>
                <v-list-item-icon>
                    <v-btn v-if="canEdit" text icon @click="EditClicked">
                        <v-icon color="secondary" size="16">fas fa-edit</v-icon>
                    </v-btn>
                    <v-btn v-if="canDelete" text icon @click="DeleteClicked">
                        <v-icon color="secondary" size="16">fas fa-trash</v-icon>
                    </v-btn>
                </v-list-item-icon>
            </v-list-item>

            <v-card-text>
                <v-container>
                    <v-row no-gutters>
                        <v-col v-for="(icon, index) in icons" :key="'icon-' + index" cols="6" class="subtitle-1">
                            <v-icon color="secondary" size="16" class="my-3">{{icon.icon}}</v-icon>
                            {{data[icon.value]}}
                        </v-col>
                    </v-row>
                </v-container>
            </v-card-text>
            <v-card-actions v-if="detailsBtn" class="justify-end">
                <v-btn text color="primary" left :to="btnPath">{{btnText}}</v-btn>
            </v-card-actions>
        </v-card>
    </v-col>
</template>

<script>
export default {
    name: "CardWithIcons",

    props: {
        'can-edit': {
            type: Boolean,
            default: false
        },
        'can-delete': {
            type: Boolean,
            default: false
        },
        title: {
            type: String,
            required: true
        },
        subtitle: {
            type: String,
            required: false
        },
        icons: {
            type: Array,
            required: true
        },
        data: {
            type: Object,
            required: true
        },
        detailsBtn: {
            type: Boolean,
            default: false
        },
        btnText: {
            type: String,
            default: 'تفاصيل'
        },
        btnPath: {
            type: String,
            required: false
        }
    },

    methods: {
        EditClicked(){
            this.$emit("editClicked", this.data.id);
        },
        DeleteClicked(){
            this.$emit("deleteClicked", this.data.id);
        }
    }
}
</script>

<style scoped>
</style>