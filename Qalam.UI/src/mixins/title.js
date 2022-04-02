export default {
    methods: {
        getTitle() {
            const { title } = this
            if (title) {
                return typeof title === 'function'
                    ? title.call(this)
                    : title
            }
        },
        test () {
            const title = this.getTitle()
            if (title) {
                document.title = title
            }
        },
    }
}