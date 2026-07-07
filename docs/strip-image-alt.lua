-- Removes alt text from images before LaTeX output.
-- Prevents \includegraphics[alt={...}] which requires graphicx >= 2022.
function Image(el)
  el.caption = {}
  return el
end
