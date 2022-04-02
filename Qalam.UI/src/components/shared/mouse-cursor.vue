<template>
    <div class="mouse-cursor" ref="mouse-cursor">
        <i :class="icon"></i>
    </div>
</template>

<script>
export default {
    name: "MouseCursor",

    props: ['icon'],

    created(){
        this.$eventBus.$on("MouseMove", this.ElementPosition);
    },

    methods: {
        mouseX(event){
            return event.clientX;
        },
        mouseY(event){
            return event.clientY;
        },
        ElementPosition(event){
            var mouse;
            mouse = {
                x: this.mouseX(event),
                y: this.mouseY(event) 
            };
            this.$refs['mouse-cursor'].style.top = mouse.y + 'px';
            this.$refs['mouse-cursor'].style.left = mouse.x + 'px';
        }
    }
}
</script>

<style scoped>
.mouse-cursor{
    position: absolute;
    top: 50%;
    left: 50%
}
</style>