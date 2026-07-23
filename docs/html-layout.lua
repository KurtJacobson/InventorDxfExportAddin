-- Appends a § anchor link to each heading for direct section linking.
-- No wrapper divs needed — layout is handled by CSS Grid on body.

local function anchor(id)
  return pandoc.RawInline('html',
    ' <a class="anchor" href="#' .. id .. '" aria-label="Link to this section">§</a>')
end

function Header(h)
  if h.identifier and h.identifier ~= '' then
    h.content:insert(anchor(h.identifier))
  end
  return h
end
