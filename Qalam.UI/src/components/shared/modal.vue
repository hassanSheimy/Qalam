<template>
    <v-dialog v-model="isOn" persistent :max-width="width" v-bind="$attrs" :transition="transition" :origin="origin" class="z-index">
        <v-card ref="modal-card">
            <v-toolbar dark color="primary">
                <v-toolbar-title class="headline">{{title}}</v-toolbar-title>
                <v-spacer />
                <v-btn icon dark @click="ModalClose">
                    <v-icon>fas fa-times</v-icon>
                </v-btn>
            </v-toolbar>
            <v-card-text>
                <slot></slot>
            </v-card-text>
            <v-card-actions>
                <v-spacer />
                <v-btn v-if="showBtn" color="primary darken-1" @click="ModalDone">{{btnText || $locales.t('submitBtn')}}</v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>

<script>
export default {
    name: "Modal",
    
    props: {
        dialog: {
            type: Boolean,
            required: true
        }, 
        width: {
            type: [Number, String],
            default: 1000
        }, 
        title: {
            type: String,
            required: true
        },
        btnText: {
            type: String,
        },
        showBtn:{
            type: Boolean,
            default: true
        },
        transition: {
            type: String,
            default: 'scale-transition'
        },
        origin: {
            type: String,
            default: 'center center'
        }
    },

    computed: {
        isOn() {
            return this.dialog;
        }
    },

    methods: {
        ModalClose(){
            this.$emit("close");
        },

        ModalDone(){
            if(!this.$parent.$refs.form || this.$parent.$refs.form.validate()){
                this.$emit("done");
            }
        }
    }
}
</script>

<style scoped>
.z-index{
    z-index: 1500;
}
</style>