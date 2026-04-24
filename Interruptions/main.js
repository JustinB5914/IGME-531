let SVG_String;
let width = 800;
let height = 600;
let lineCountX = 80;
let lineCountY = 60;
let lineLength = 18;
let padding = 20;
let gapCount = Math.floor(Math.random() * 5);
let gapArrayX = new Array;
let gapArrayY = new Array;

document.addEventListener('DOMContentLoaded', function() {
    const container = document.getElementById("svgContainer");
    
    SVG_String = "<svg width=\"" + width + "\" height=\"" + height + "\">";

    for(var i = 0; i < gapCount; i++)
    {
        if(Math.round(Math.random()) == 0)
        {
            let gapHeight = Math.floor(Math.random() * 4) + 3;
            let gapWidth = Math.floor(Math.random() * 2) + 2;

            let gapStartX = Math.floor(Math.random() * (lineCountX - gapWidth));
            let gapStartY = Math.floor(Math.random() * (lineCountY - gapHeight));

            console.log(gapStartX);
            console.log(gapStartY);

            for(var x = gapStartX; x < gapWidth + gapStartX; x++)
            {
                for(var y = gapStartY; y < gapHeight + gapStartY; y++)
                {
                    gapArrayX.push(x);
                    gapArrayY.push(y);
                }
            }
        }
        else
        {
            let gapHeight = Math.floor(Math.random() * 4) + 2;
            let gapWidth = Math.floor(Math.random() * 7) + 3;

            let gapStartX = Math.floor(Math.random() * (lineCountX - gapWidth));
            let gapStartY = Math.floor(Math.random() * (lineCountY - gapHeight));

            console.log(gapStartX);
            console.log(gapStartY);

            for(var x = gapStartX; x < gapWidth + gapStartX; x++)
            {
                for(var y = gapStartY; y < gapHeight + gapStartY; y++)
                {
                    gapArrayX.push(x);
                    gapArrayY.push(y);
                }
            }
        }
    }

    for(var i = 0; i < lineCountX; i++)
    {
        for(var j = 0; j < lineCountY; j++)
        {
            let gap = false;
            for(var x = 0; x < gapArrayX.length; x++)
            {
                for(var y = 0; y < gapArrayY.length; y++)
                {
                    if(gapArrayX[x] == i && gapArrayY[y] == j)
                    {
                        gap = true;
                    }
                }
            }

            var lineCenterX = (((width - padding*2) / lineCountX) * i) + padding;
            var lineCenterY = ((height - padding*2) / lineCountY) * j + padding;
            var angle = Math.random() * (Math.PI);

            if(gap == false)
            {
                SVG_String += "<line x1=\"";
                SVG_String += (lineCenterX + Math.cos(angle) * lineLength / 2);
                SVG_String += "\" y1=\"";
                SVG_String += (lineCenterY + Math.sin(angle) * lineLength / 2);
                SVG_String += "\" x2=\"";
                SVG_String += (lineCenterX - Math.cos(angle) * lineLength / 2);
                SVG_String += "\" y2=\"";
                SVG_String += (lineCenterY - Math.sin(angle) * lineLength / 2);
                SVG_String += "\" style=\"stroke:black;stroke-width:1\" />";
            }   
        }
    }

    SVG_String += "</svg>";

    //console.log(SVG_String);
    container.innerHTML = SVG_String;
});