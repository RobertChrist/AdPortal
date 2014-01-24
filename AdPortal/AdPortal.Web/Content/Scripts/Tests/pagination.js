/* 
 * Unit testing pagination.js
 * 
 * @Requires pagination.js
 * @Requires entire Jasmine Library
 */
describe('Pagination Tests', function () {
    // Mock QueryString.js
    window.QueryString = {
        getQueryStringParams: function () {
            return {};
        },
        updateQueryString: function () { }
    };

    it("Displays the first, last, and nearby pages when there are lots of pages, and we are in the middle.", function () {
        var menu = window.Pagination.getPaginationMenu(30, 15);

        expect(menu.length).toEqual(12);
        expect(menu[0].pageNum).toEqual(1);
        expect(menu[1].pageNum).toEqual(11);
        expect(menu[2].pageNum).toEqual(12);
        expect(menu[3].pageNum).toEqual(13);
        expect(menu[4].pageNum).toEqual(14);
        expect(menu[5].pageNum).toEqual(15);
        expect(menu[6].pageNum).toEqual(16);
        expect(menu[7].pageNum).toEqual(17);
        expect(menu[8].pageNum).toEqual(18);
        expect(menu[9].pageNum).toEqual(19);
        expect(menu[10].pageNum).toEqual(20);
        expect(menu[11].pageNum).toEqual(30);
    });

    it("Can handle 0 pages.", function () {
        var menu = window.Pagination.getPaginationMenu(0, 0);
        
        expect(menu.length).toEqual(0);
    });

    it("Can handle 1 pages.", function () {
        var menu = window.Pagination.getPaginationMenu(1, 0);
        console.log(menu);
        expect(menu.length).toEqual(1);
        expect(menu[0].pageNum).toEqual(1);
        expect(menu[0].template).not.toBeNull();
    });

    it("Displays 2 pages, when there are only two pages, and we are on page two.", function () {
        var menu = window.Pagination.getPaginationMenu(2, 1);

        expect(menu.length).toEqual(2);
        expect(menu[0].pageNum).toEqual(1);
        expect(menu[1].pageNum).toEqual(2);

        expect(menu[0].template).not.toBeNull();
        expect(menu[1].template).not.toBeNull();
    });

    it("Displays 2 pages, when there are only two pages, and we are on page one.", function () {
        var menu = window.Pagination.getPaginationMenu(2, 1);

        expect(menu.length).toEqual(2);
        expect(menu[0].pageNum).toEqual(1);
        expect(menu[1].pageNum).toEqual(2);

        expect(menu[0].template).not.toBeNull();
        expect(menu[1].template).not.toBeNull();
    });

    it("Displays 3 pages, when there are 3 pages, and we are on page 1.", function () {
        var menu = window.Pagination.getPaginationMenu(3, 1);

        expect(menu.length).toEqual(3);
        expect(menu[0].pageNum).toEqual(1);
        expect(menu[1].pageNum).toEqual(2);
        expect(menu[2].pageNum).toEqual(3);

        expect(menu[0].template).not.toBeNull();
        expect(menu[1].template).not.toBeNull();
        expect(menu[2].template).not.toBeNull();
    });

    it("Displays page 1, and 3-9 when there are 9 pages, we are on page 8.", function () {
        var menu = window.Pagination.getPaginationMenu(9, 7);

        expect(menu.length).toEqual(8);
        expect(menu[0].pageNum).toEqual(1);
        expect(menu[1].pageNum).toEqual(3);
        expect(menu[2].pageNum).toEqual(4);
        expect(menu[3].pageNum).toEqual(5);
        expect(menu[4].pageNum).toEqual(6);
        expect(menu[5].pageNum).toEqual(7);
        expect(menu[6].pageNum).toEqual(8);
        expect(menu[7].pageNum).toEqual(9);
    });

    it("Displays page 1, and 4-9 when there are 9 pages, we are on page 9.", function () {
        var menu = window.Pagination.getPaginationMenu(9, 8);

        expect(menu.length).toEqual(7);
        expect(menu[0].pageNum).toEqual(1);
        expect(menu[1].pageNum).toEqual(4);
        expect(menu[2].pageNum).toEqual(5);
        expect(menu[3].pageNum).toEqual(6);
        expect(menu[4].pageNum).toEqual(7);
        expect(menu[5].pageNum).toEqual(8);
        expect(menu[6].pageNum).toEqual(9);
    });

    it("Displays page 1-7, and page 9, when there are 9 pages, we are on page 2.", function () {
        var menu = window.Pagination.getPaginationMenu(9, 1);

        expect(menu.length).toEqual(7);
        expect(menu[0].pageNum).toEqual(1);
        expect(menu[1].pageNum).toEqual(2);
        expect(menu[2].pageNum).toEqual(3);
        expect(menu[3].pageNum).toEqual(4);
        expect(menu[4].pageNum).toEqual(5);
        expect(menu[5].pageNum).toEqual(6);
        expect(menu[6].pageNum).toEqual(9);
    });

    it("Displays page 1-5, and page 9 when there are 9 page, we are on page 1.", function () {
        var menu = window.Pagination.getPaginationMenu(9, 0);

        expect(menu.length).toEqual(6);
        expect(menu[0].pageNum).toEqual(1);
        expect(menu[1].pageNum).toEqual(2);
        expect(menu[2].pageNum).toEqual(3);
        expect(menu[3].pageNum).toEqual(4);
        expect(menu[4].pageNum).toEqual(5);
        expect(menu[5].pageNum).toEqual(9);
    });
});