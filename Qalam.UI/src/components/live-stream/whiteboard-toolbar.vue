<template>
    <v-toolbar dense floating class="whiteboard-toolbar justify-center text-center">
        <p :class="$vuetify.rtl ? 'mx-10' : 'mr-10'" class="my-3 text-center student-count">
            <i class="fas fa-user mx-3" aria-hidden="true" /> 
            <span v-text="numberOfStudent"/>    
        </p>
        <button v-for="(button, index) in buttons" :key="'whiteboard-toolbar-button-' + index" 
        @click="Click(button)" class="ma-3 mx-6">
            
            <i v-if="index < buttons.length - 1" :class="button.icon" aria-hidden="true" />
            <i v-else :class="`${button.icon} ${$vuetify.rtl ? 'mr-10' : 'ml-10'}`" aria-hidden="true"/>
        </button>
    </v-toolbar>
</template>

<script>
export default {
    name: "WhiteboardToolbar",

    data: () => ({
        studentsCount: 994651,
        buttons: [
            { event: 'whiteboard-pen', icon: 'fas fa-pen' },
            { event: 'whiteboard-color', icon: 'fas fa-tint' },
            { event: 'whiteboard-redo', icon: 'fas fa-redo' },
            { event: 'whiteboard-undo', icon: 'fas fa-undo' },
            { event: 'whiteboard-trash', icon: 'fas fa-trash' },
            { event: 'whiteboard-eraser', icon: 'fas fa-eraser' },
            { event: 'whiteboard-compress', icon: 'fas fa-compress' },
            { event: 'whiteboard-exit', icon: 'fas fa-door-open' },
        ]
    }),

    computed: {
        numberOfStudent(){
            return `${this.studentsCount} ${this.$locales.t('student')}`;
        }
    },

    methods: {
        Click(button){
            this.$eventBus.$emit(button.event);
        }
    }
}
</script>

<style scoped>
.whiteboard-toolbar{
    position: absolute;
    margin-top: 15px;
}
.whiteboard-toolbar .button{
    margin: 10px;
}
.student-count{
    clear: both;
    display: inline-block;
    overflow: hidden;
    white-space: nowrap;
}
</style>